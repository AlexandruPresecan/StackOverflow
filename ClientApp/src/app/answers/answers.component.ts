import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Answer } from '../../models/answer.model';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.css'],
})

export class AnswersComponent {

  @Input() answers: Answer[] = [];

  constructor(private router: Router) {

  }

  public redirectToQuestion(id: number): void {
    this.router.navigate(['questions', id]);
  }
}
