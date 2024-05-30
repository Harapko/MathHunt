import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient: HttpClient) { }

  get<T>(url: string): Observable<T>{
    return this.httpClient.get<T>(url) as Observable<T>
  }

  post<T>(url: string, body: any): Observable<T>{
    return this.httpClient.post<T>(url, body) as Observable<T>
  }

  put<T>(url: string, body: any): Observable<T>{
    return this.httpClient.put<T>(url, body) as Observable<T>
  }

  delete<T>(url: string): Observable<T>{
    return this.httpClient.delete<T>(url) as Observable<T>
  }
}
