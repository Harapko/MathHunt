import { Injectable } from '@angular/core';
import {ApiService} from "../api.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private apiService: ApiService) { }

  registerUser = (url: string, formData: FormData): Observable<any> =>{
    return this.apiService.post(url, formData);
  }
}
