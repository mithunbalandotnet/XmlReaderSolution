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
    return this.apiService.post({ postData: xmlContent }, 'api/extract');
  }
}
