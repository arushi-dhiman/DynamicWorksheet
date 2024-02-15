
const sql = require('mssql/msnodesqlv8');
const fs = require('fs');
const outputFile = 'worksheetTemplates.json'

const config = {
  server: 'DESKTOP-6DK60DE',
  database: 'Dynamic_Templates',
  driver: 'msnodesqlv8',
  options: {
    trustedConnection: true, // Use Windows Authentication
    encrypt: false, // Set to true if your SQL Server uses SSL
    enableArithAbort: true, 
  }
}

// Create a pool connection
const pool = new sql.ConnectionPool(config);

// Connect to the database
pool.connect().then(() => {
  console.log('DB connect successfully ')
  // Create a request object
  const request = new sql.Request(pool);

  // Define the name of your stored procedure
  const storedProcedureName = 'sp_getAllTemplate';

  // Execute the stored procedure
  request.execute(storedProcedureName, (err, result) => {
    if (err) {
      console.error(err);
    } else {
      console.log('stored procedure call successful')
      // Specify the output file
      result.recordset[0].Templates = JSON.parse(result.recordset[0].Templates)
      fs.writeFileSync(outputFile, JSON.stringify(result.recordset[0], null, 2));

      console.log("Stored procedure data pushed to file worksheetTemplates.json successfully");
    }

    // Close the connection pool
    pool.close();
    console.log("Data base connection closed");

  });
}).catch((err) => {
  console.error(err);
});
