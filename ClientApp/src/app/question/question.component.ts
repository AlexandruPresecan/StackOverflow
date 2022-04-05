import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionsService } from '../../services/questions.service';
import { Question } from '../../models/question.model';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
  providers: [QuestionsService]
})

export class QuestionComponent {

  public question!: Question;

  constructor(questionsService: QuestionsService, route: ActivatedRoute) {
    questionsService.getById(route.snapshot.params.id).subscribe(result => {
      this.question = result;
    }, error => console.error(error));
  }
}
