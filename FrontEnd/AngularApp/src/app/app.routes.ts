import { Routes, RouterModule } from '@angular/router'
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import path from "node:path";
import {HomePageComponent} from "./home-page/home-page.component";


export const routes: Routes = [
  {path: 'register' , component: RegisterComponent },
  {path: 'login' , component: LogInComponent},
  {path: 'home-page' , component: HomePageComponent}



];


