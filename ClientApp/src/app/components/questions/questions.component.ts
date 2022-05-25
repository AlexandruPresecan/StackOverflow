import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Question } from '../../../models/question.model';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css'],
})

export class QuestionsComponent {

  @Input() questions: Question[] = [];

  constructor(private router: Router) {

  }

  redirectToQuestion(id: number): void {
    this.router.navigate(['questions', id]);
  }

  redirectToAuthor(author: User | undefined): void {
    if (author)
      this.router.navigate(['users', author.id]);
  }
}
