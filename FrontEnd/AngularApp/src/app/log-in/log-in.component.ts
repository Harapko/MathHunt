import {Component, computed, Input} from '@angular/core';
import {RouterLink, RouterOutlet} from "@angular/router";




@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent {

  @Input() position!: { top: number, left: number };



}
