import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-page-hunter',
  standalone: true,
    imports: [
        RouterLink
    ],
  templateUrl: './page-hunter.component.html',
  styleUrl: './page-hunter.component.scss'
})
export class PageHunterComponent {

  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }

}
