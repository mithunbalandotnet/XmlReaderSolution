import { Injectable } from '@angular/core';
import { Product } from '../model/product';
import { ApiService } from './api-service';
import { error } from 'console';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReadXmlService {
  constructor(private apiService: ApiService) { }
  parseMailContent(xmlContent: string): Observable<Product> {
    return new Observable<Product>(observer => {
      this.apiService.post({ postData: xmlContent }, 'api/extract').subscribe(response => {
        console.log('API Response:', response);
        observer.next(response);
        observer.complete();
      }, error => {
        console.error('API Error:', error);
        observer.error(error);
      });
    });
  }
}
