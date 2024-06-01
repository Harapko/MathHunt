import { Component } from '@angular/core';

@Component({
  selector: 'app-page-user',
  standalone: true,
  imports: [],
  templateUrl: './page-user.component.html',
  styleUrl: './page-user.component.scss'
})
export class PageUserComponent {

  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }


  // isDropdownOpen: boolean = false;
  //  options: string[] = ['Опція 1', 'Опція 2', 'Опція 3', 'Опція 4', 'Опція 5', 'Опція 6', 'Опція 7', 'Опція 8', 'Опція 9', 'Опція 10', 'Опція 11', 'Опція 12', 'Опція 13', 'Опція 14', 'Опція 15', 'Опція 16', 'Опція 17', 'Опція 18', 'Опція 19', 'Опція 20']; // Додайте більше опцій, якщо потрібно
  //
  // toggleDropdown() {
  //   this.isDropdownOpen = !this.isDropdownOpen;
  // }
  //
  // selectOption(option: string) {
  //   console.log('Вибрано опцію:', option);
  //   // Додайте тут ваші дії при виборі опції
  // }
}
