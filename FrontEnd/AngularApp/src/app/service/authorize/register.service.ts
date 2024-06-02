import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormGroup} from "@angular/forms";
import {Router} from "@angular/router";
import {map, Observable} from "rxjs";
import {response} from "express";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  isLoading: boolean = false;
  message: string = '';
  constructor(private httpClient: HttpClient, private router: Router) { }

  registerUser(userData: FormGroup) : Observable<string>{
    return this.httpClient.post<string>("http://localhost:5117/registerUser", userData)
      .pipe(map(response => {
        return response;
      }))
  }

}
