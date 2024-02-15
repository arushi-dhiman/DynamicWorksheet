using DynamicRepository.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Metadata;

namespace DynamicRepository
{

    public class DbHelper : IDBHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string GetFindings()
        {
            string findingsJson = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var str = @"EXECUTE [dbo].[sp_GetFindings]";
                    SqlCommand sqlCommand = new SqlCommand(str, conn);

                    conn.Open();
                    SqlDataReader rdr = sqlCommand.ExecuteReader();
                    while (rdr.Read())
                    {
                        findingsJson = rdr["OptionStatementFindings"].ToString();                  
                    }
                    return findingsJson;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public IEnumerable<RecordFinidngModel> GetRecords(int userId, int templateId)
        {
            List<RecordFinidngModel> recordFinidngs = new List<RecordFinidngModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var str = @"EXECUTE [dbo].[sp_GetTemplateRecords]
                              @UserId,
                              @TemplateId";
                    SqlCommand sqlCommand = new SqlCommand(str, conn);
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlCommand.Parameters.AddWithValue("@TemplateId", templateId);



                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Record record = new Record();

                        record.Id = Convert.ToInt32(row["Id"]);
                        record.UserId = Convert.ToInt32(row["UserId"]);
                        record.FieldId = Convert.ToInt32(row["FieldId"]);
                        record.FieldValue = row["FieldValue"].ToString();
                        record.IsMultipleValues = Convert.ToBoolean(row["IsMultipleValues"]);

                            FindingJson finding = new FindingJson
                        {
                            FindingId = DBNull.Value.Equals(row["FindingId"]) ? 0 : Convert.ToInt32(row["FindingId"]),
                            FindingTitle = row["FindingTitle"] != DBNull.Value ? row["FindingTitle"].ToString() : null,
                            FindingOrder = DBNull.Value.Equals(row["FindingOrder"]) ? 0 : Convert.ToInt32(row["FindingOrder"]),
                            Statements = new List<StatementJson>()
                        };

                        StatementJson statement = new StatementJson
                        {
                            StatementId = DBNull.Value.Equals(row["StatementId"]) ? 0 : Convert.ToInt32(row["StatementId"]),
                            Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                            FieldIds = new List<Field>()
                        };

                        Field field = new Field
                        {
                            FieldId = Convert.ToInt32(row["FieldId"]),
                            FieldOptionId = DBNull.Value.Equals(row["FieldOptionId"]) ? 0 : Convert.ToInt32(row["FieldOptionId"])
                        };

                        statement.FieldIds.Add(field);
                        finding.Statements.Add(statement);
                        RecordFinidngModel recordFinidngModel = new RecordFinidngModel
                        {
                            Records = new List<Record> { record },
                            Findings = new List<FindingJson> { finding }
                        };

                        recordFinidngs.Add(recordFinidngModel);
                
                    }
                    
                    return recordFinidngs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public DBResponse SaveRecords(SaveTemplateDataModel record)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var str = @"EXECUTE [dbo].[sp_saveTemplateRecords] 
	                          @IsClear,
                              @UserId,
                              @TemplateId,
                              @TemplateData";
                           

                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand(str, conn);
                    sqlCommand.CommandType = CommandType.Text;

                    DataTable templateData = new DataTable();
                    templateData.Columns.Add("Id", typeof(int));
                    templateData.Columns.Add("UserId", typeof(int));
                    templateData.Columns.Add("FieldId", typeof(int));
                    templateData.Columns.Add("FieldValue", typeof(string));
                    templateData.Columns.Add("IsMultipleValues", typeof(bool));


                    sqlCommand.Parameters.AddWithValue("@IsClear", record.IsClear ? 1 : 0);
                    sqlCommand.Parameters.AddWithValue("@UserId", record.UserId);
                    sqlCommand.Parameters.AddWithValue("@TemplateId", record.TemplateId);
                    foreach (var templateField in record.TemplateFields)
                    {
                        templateData.Rows.Add(templateField.Id, templateField.UserId, templateField.FieldId,  templateField.FieldValue, templateField.IsMultipleValues);
                    }

                    SqlParameter tvpParam = sqlCommand.Parameters.AddWithValue("@TemplateData", templateData);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.UT_TemplateFields";
                    int dbEffectedRows = sqlCommand.ExecuteNonQuery();
                    if (dbEffectedRows <= 0)
                        {
                            return new DBResponse { Status = "FAILED", Message = "Records not added, please try again!" };
                        }                 
                    return new DBResponse { Status = "SUCCESS", Message = "Records added successfully!" };
                }
            }
            catch (Exception ex)
            {
                return new DBResponse { Status = "ERROR", Message = "Records not added, please try again!" };
            }
        }

        public IEnumerable<DynamicField> GetDynamicFields(int templateId, int userId)
        {
            List<DynamicField> dynamicFields = new List<DynamicField>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var str = @"EXECUTE [dbo].[sp_GetDynamicFields]
                        @TemplateId,
                        @UserId";
                    SqlCommand sqlCommand = new SqlCommand(str, conn);
                    sqlCommand.Parameters.AddWithValue("@TemplateId", templateId);
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);



                    conn.Open();
                    SqlDataReader rdr = sqlCommand.ExecuteReader();
                    while (rdr.Read())
                    {
                        DynamicField dynamicField = new DynamicField
                        {
                            DynamicFieldId = Convert.ToInt32(rdr["Id"]),
                            FieldId = Convert.ToInt32(rdr["FieldId"]),
                            FieldName = rdr["FieldName"].ToString(),
                            Label = rdr["Label"].ToString(),
                            IsDynamic = Convert.ToBoolean(rdr["IsDynamic"]),
                            UserId = Convert.ToInt32(rdr["UserId"]),
                        };

                        dynamicFields.Add(dynamicField);
                    }

                    return dynamicFields;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
    
}
