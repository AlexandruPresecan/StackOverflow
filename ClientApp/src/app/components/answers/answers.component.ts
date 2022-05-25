import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Answer } from '../../../models/answer.model';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.css'],
})

export class AnswersComponent {

  @Input() answers: Answer[] = [];
  @Input() allowVotes!: boolean;
  @Output() onVote = new EventEmitter();
}
