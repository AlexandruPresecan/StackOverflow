import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from '../../../models/question.model';
import { QuestionsService } from '../../../services/questions.service';

@Component({
  selector: 'app-questions-page',
  templateUrl: './questions-page.component.html',
  styleUrls: ['./questions-page.component.css'],
  providers: [QuestionsService]
})

export class QuestionsPageComponent {

  questions: Question[] = [];

  constructor(questionsService: QuestionsService, private router: Router, route: ActivatedRoute) {

    route.queryParams.subscribe(params => {

      if (params.name)
        questionsService.getByName(params.name).subscribe(result => {
          this.questions = result;
        }, error => console.log(error));
      else
        questionsService.getAll().subscribe(result => {
          this.questions = result;
        }, error => console.log(error));
    });
  }

  createQuestion(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    this.router.navigate(['create-question']);
  }
}
