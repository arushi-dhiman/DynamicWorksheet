import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class FindingApiService {

  private apiUrl = 'https://localhost:7055/Finding/GetFindings';
  constructor(private httpClient: HttpClient) { }
  //http call to fetch Findings data from database which gives the JSON output from our API

  getFindingsList(): Observable<any> {
    return this.httpClient.get(this.apiUrl);
  }

}

