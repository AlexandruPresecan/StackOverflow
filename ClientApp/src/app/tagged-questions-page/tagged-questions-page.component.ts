import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Question } from '../../models/question.model';
import { QuestionsService } from '../../services/questions.service';

@Component({
  selector: 'app-tagged-questions-page',
  templateUrl: './tagged-questions-page.component.html',
  styleUrls: ['./tagged-questions-page.component.css'],
  providers: [QuestionsService]
})

export class TaggedQuestionsPageComponent {

  questions: Question[] = [];
  tag: string = "";

  constructor(questionsService: QuestionsService, route: ActivatedRoute) {

      this.tag = route.snapshot.params.tag;

      questionsService.getByTag(this.tag).subscribe(result => {
        this.questions = result;
      }, error => console.error(error));
  }
}
