import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./component/header/header.component";
import {RegisterComponent} from "./component/register/register.component";
import {LogInComponent} from "./component/log-in/log-in.component";
import {MainComponent} from "./component/main/main.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, RegisterComponent, LogInComponent, MainComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AngularApp';
  protected readonly HeaderComponent = HeaderComponent;
}
