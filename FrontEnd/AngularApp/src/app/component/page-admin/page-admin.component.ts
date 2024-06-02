import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {AuthService} from "../../service/authorize/auth.service";
import {UserService} from "../../service/userService/user.service";

@Component({
  selector: 'app-page-admin',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './page-admin.component.html',
  styleUrl: './page-admin.component.scss'
})
export class PageAdminComponent {

  constructor(private authService: AuthService, private userService: UserService) {
  }

  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }

  protected readonly AuthService = AuthService;

  logout(){
    console.log(this.userService.role)
    this.authService.logout();
    console.log(this.userService.role)

  }

}
