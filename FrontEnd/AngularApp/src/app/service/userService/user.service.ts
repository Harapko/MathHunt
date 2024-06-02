import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {map, Observable} from "rxjs";
import { UserResponse } from "../../models/user/user-response";
import {response} from "express";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  email: string = '';
  name: string = '';
  surname: string = '';
  phoneNumber: string = '';
  role: string = '';



  constructor(
    private httpClient: HttpClient,
  ) { }

  getAllUser(): Observable<UserResponse[]> {
    return this.httpClient.get<any>('http://localhost:5117/getUser')
      .pipe(
        map(response => response.$values as UserResponse[])
      );
  }


  getUserInfo(name: string): void {
    this.httpClient.post<UserResponse>("http://localhost:5117/getUserByName", { name: name })
      .subscribe((response: UserResponse) => {
          this.name = response.name;
          this.surname = response.surname;
          this.email = response.email;
          this.phoneNumber = response.phoneNumber;
          this.role = response.role;
        },
        error => {
          console.error('Error fetching user info:', error);
        }
      );
  }

  logoutUser(){
    this.name = '';
    this.surname = '';
    this.email = '';
    this.phoneNumber = '';
    this.role = '';
  }


}
