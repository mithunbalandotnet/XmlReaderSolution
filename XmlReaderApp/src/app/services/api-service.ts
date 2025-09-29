import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private httpClient: HttpClient) { }

  post<T1>(postData: T1, url: string) : Observable<any> {
    const fullUrl = environment.apiUrl + url;
    return this.httpClient.post(fullUrl, postData);
  }
}
