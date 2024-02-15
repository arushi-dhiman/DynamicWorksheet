import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TemplatesService {

  private saveApiUrl = 'https://localhost:7055/Records/SaveRecords';
  private getApiUrl = 'https://localhost:7055/Records/GetRecords';
  private getDynamicFieldsUrl = 'https://localhost:7055/Fields/GetDynamicFields';
  constructor(private httpClient: HttpClient) { }

  saveRecords(data : any): Observable<any> {
    return this.httpClient.post(this.saveApiUrl, data);
  }
  getRecords(userId: any, templateId: any): Observable<any> {
    const apiUrl = `${this.getApiUrl}?UserId=${userId}&templateId=${templateId}`;
    return this.httpClient.get(apiUrl);
  }

 
  getDynamicFields(templateId: any, userId: any): Observable<any> {
    const apiUrl = `${this.getDynamicFieldsUrl}?templateId=${templateId}&userId=${userId}`;
    return this.httpClient.get(apiUrl).pipe(map((response: any) => {
      response = response.map((record: any) => {
        if (record.isDynamic) {
          record.fieldName = record.fieldName + '_' + record.fieldId;
        }
        return record;
      });
      return response;
    }));
  }


}

