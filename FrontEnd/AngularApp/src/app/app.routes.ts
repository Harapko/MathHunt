import { Routes, RouterModule } from '@angular/router'
import { RegisterComponent } from './register/register.component';
import { LogInComponent } from './log-in/log-in.component';
import {HomePageComponent} from "./home-page/home-page.component";
import {MainComponent} from "./main/main.component";
import {PageUserComponent} from "./page-user/page-user.component";


export const routes: Routes = [
  {path: '' , component: MainComponent },
  {path: 'register' , component: RegisterComponent },
  {path: 'login' , component: LogInComponent},
  {path: 'profile' , component: PageUserComponent},
  {path: 'home-page' , component: HomePageComponent}



];


