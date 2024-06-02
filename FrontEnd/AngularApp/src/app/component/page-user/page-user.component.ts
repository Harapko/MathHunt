import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../../service/authorize/auth.service";
import {UserService} from "../../service/userService/user.service";
import {UserResponse} from "../../models/user/user-response";

@Component({
  selector: 'app-page-user',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './page-user.component.html',
  styleUrl: './page-user.component.scss'
})

export class PageUserComponent implements OnInit {
  skillNames: string[] = [];
  skillAllList: string[] = [];
  user!: UserResponse;
  addSkillForm!: FormGroup;
  isLoading: boolean = false;
  message: string = '';

  constructor(private http: HttpClient, private userService: UserService, public authService: AuthService) {
  }

  ngOnInit(): void {
    this.user = this.userService;

      this.getAllSkill();
      this.getSkillNames();
      this.initForm();


    const trigger = document.getElementById("trigger");
    const list = document.getElementById("list");
    const option1 = document.getElementById("option1") as HTMLInputElement;
    const option2 = document.getElementById("option2") as HTMLInputElement;

    if (trigger) {
      trigger.addEventListener("click", () => {
        if (list) {
          list.style.display = (list.style.display === "none") ? "block" : "none";
        }
      });
    }

    if (option1) {
      option1.addEventListener("change", () => {
        if (option1.checked) {
          console.log("Опція 1 обрана");
          // Додайте код, який потрібно виконати, коли опція 1 обрана
        } else {
          console.log("Опція 1 скасована");
          // Додайте код, який потрібно виконати, коли опція 1 скасована
        }
      });
    }

    if (option2) {
      option2.addEventListener("change", () => {
        if (option2.checked) {
          console.log("Опція 2 обрана");
          // Додайте код, який потрібно виконати, коли опція 2 обрана
        } else {
          console.log("Опція 2 скасована");
          // Додайте код, який потрібно виконати, коли опція 2 скасована
        }
      });
    }
  }

  getSkillNames(): void {
    // Передаем параметр userName в URL запроса
    this.http.get<any>('http://localhost:5117/getSkillsUser', {params: {userName: this.user.name}})
      .subscribe(
        (response: any) => {
          // Проверяем, что ответ содержит массив значений
          if (response && response.values && Array.isArray(response.values)) {
            // Присваиваем полученные значения переменной skillNames
            this.skillNames = response.values;
          } else {
            console.error('Unexpected response format:', response);
          }
        },
        error => {
          console.error('Error fetching skill names:', error)
        }
      );
  }

  private initForm() {
    this.addSkillForm = new FormGroup({
      userName: new FormControl(`${this.user.email}`, Validators.required),
      skillName: new FormControl('', Validators.required),
    });
  }

  // add skill to user
  onSubmit(): void {
    if (this.addSkillForm.valid) {
      this.isLoading = true;
      const formData = this.addSkillForm.value;

      this.http.post('http://localhost:5117/addSkillToUser', formData).subscribe(
        response => {
          this.getSkillNames();
          this.isLoading = false;
          this.message = 'User registered successfully!';
          console.log('User registered', response);
        },
        error => {

          this.isLoading = false;
          this.message = 'Registration failed. Please try again.';
          console.error('Registration error', error);
        }
      );
    } else {

      this.message = 'Please fill out all fields correctly.';
    }
  }

  getAllSkill() {
    this.http.get<any>('http://localhost:5117/getSkill')
      .subscribe(
        (response: any) => {
          console.log('Server response:', response); // Отладочный вывод

          // Проверьте, содержит ли ответ массив $values
          if (response && response.$values && Array.isArray(response.$values)) {
            // Извлеките skillName из каждого объекта в массиве $values
            this.skillAllList = response.$values.map((item: any) => item.skillName);
          } else {
            console.error('Unexpected response format:', response);
          }
        },
        error => {
          console.error('Error fetching skill names:', error);
        }
      );
  }


  isBlockVisible: boolean = false;

  toggleBlock() {
    this.isBlockVisible = !this.isBlockVisible;
  }



}
