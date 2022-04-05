import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../models/question.model';
import { QuestionsService } from '../../services/questions.service';

@Component({
  selector: 'app-questions-page',
  templateUrl: './questions-page.component.html',
  styleUrls: ['./questions-page.component.css'],
  providers: [QuestionsService]
})

export class QuestionsPageComponent {

  questions: Question[] = [];

  constructor(questionsService: QuestionsService, private router: Router, private route: ActivatedRoute) {
      questionsService.getAll().subscribe(result => {
        this.questions = result;
      }, error => console.error(error));
  }

  public createQuestion(): void {
    this.router.navigate(['../', 'create-question'], { relativeTo: this.route });
  }
}
