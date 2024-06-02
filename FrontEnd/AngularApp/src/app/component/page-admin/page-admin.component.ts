import { Component } from '@angular/core';

@Component({
  selector: 'app-page-admin',
  standalone: true,
  imports: [],
  templateUrl: './page-admin.component.html',
  styleUrl: './page-admin.component.scss'
})
export class PageAdminComponent {


  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }
}
