import { Routes, RouterModule } from '@angular/router'
import path from "node:path";
import {HomePageComponent} from "./component/home-page/home-page.component";
import {LogInComponent} from "./component/log-in/log-in.component";
import {RegisterComponent} from "./component/register/register.component";
import {PageUserComponent} from "./component/page-user/page-user.component";
import {PageHunterComponent} from "./component/page-hunter/page-hunter.component";
import {UserComponent} from "./component/user/user.component";
import {PageAdminComponent} from "./component/page-admin/page-admin.component";


export const routes: Routes = [
  {path: '' , component: HomePageComponent },
  {path: 'login' , component: LogInComponent },
  {path: 'register' , component: RegisterComponent},
  {path: 'home-page' , component: HomePageComponent},
  {path: 'user-page' , component: PageUserComponent},
  {path: 'hunter-page' , component: PageHunterComponent},
  {path: 'user' , component: UserComponent},
  {path: 'admin-page' , component: PageAdminComponent},
];

