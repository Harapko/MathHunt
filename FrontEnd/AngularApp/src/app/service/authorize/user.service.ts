import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userNameSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor() {}

  setUserName(userName: string) {
    this.userNameSubject.next(userName);
  }

  getUserName(): Observable<string> {
    return this.userNameSubject.asObservable();
  }
}
