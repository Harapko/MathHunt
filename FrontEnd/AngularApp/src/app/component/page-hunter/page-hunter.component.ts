import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {AuthService} from "../../service/authorize/auth.service";
import {AllUserList} from "../../models/user/allUserList";
import {UserService} from "../../service/userService/user.service";

@Component({
  selector: 'app-page-hunter',
  standalone: true,
    imports: [
        RouterLink
    ],
  templateUrl: './page-hunter.component.html',
  styleUrl: './page-hunter.component.scss'
})
export class PageHunterComponent implements OnInit{
userList!: AllUserList[];
  constructor(public authService: AuthService, private userService: UserService) {
  }

  ngOnInit(): void {
        this.allUserList();
    }


    allUserList(){
      this.userService.getAllUser().subscribe((response: any) => {
        this.userList = response;

      })
    }

  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }

}
