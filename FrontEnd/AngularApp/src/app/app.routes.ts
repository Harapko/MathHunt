import { Routes, RouterModule } from '@angular/router'
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import path from "node:path";
import {HomePageComponent} from "./home-page/home-page.component";
import {MainComponent} from "./main/main.component";
import {PageUserComponent} from "./page-user/page-user.component";


export const routes: Routes = [
  {path: '' , component: HomePageComponent },
  {path: 'login' , component: LogInComponent },
  {path: 'register' , component: RegisterComponent},
  {path: 'home-page' , component: HomePageComponent},
  {path: 'user-page' , component: PageUserComponent},
];


