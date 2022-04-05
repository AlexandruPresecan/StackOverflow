import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-votes',
  templateUrl: './votes.component.html',
  styleUrls: ['./votes.component.css'],
})

export class VotesComponent {
  @Input() votes!: Number;
}
