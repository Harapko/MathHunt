import { Component } from '@angular/core';
import {HeaderComponent} from "../header/header.component";
import {PageUserComponent} from "../page-user/page-user.component";
import {PageHunterComponent} from "../page-hunter/page-hunter.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    HeaderComponent,
    PageUserComponent,
    PageHunterComponent
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
