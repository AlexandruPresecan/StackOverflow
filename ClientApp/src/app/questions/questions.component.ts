import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { getBaseUrl } from '../../main';
import { Question } from '../../models/question.model';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css'],
})

export class QuestionsComponent {

  @Input() questions: Question[] = [];

  constructor(private router: Router) {

  }

  public redirectToQuestion(id: number): void {
    this.router.navigate(['questions', id]);
  }
}
