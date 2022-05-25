import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Question } from '../../../models/question.model';
import { QuestionsService } from '../../../services/questions.service';

@Component({
  selector: 'app-create-question-page',
  templateUrl: './create-question-page.component.html',
  styleUrls: ['./create-question-page.component.css'],
  providers: [QuestionsService]
})

export class CreateQuestionPageComponent {

  @ViewChild('title') title!: ElementRef;
  @ViewChild('text') text!: ElementRef;
  @ViewChild('tags') tags!: ElementRef;

  questionTags: string[] = [];

  constructor(private questionsService: QuestionsService, private router: Router) {

  }

  setTags(): void {
    this.questionTags = this.tags.nativeElement.value.split(" ").filter((tag: string) => tag != "");
  }

  createQuestion(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    const question: Question = {
      id: 0,
      voteCount: 0,
      title: this.title.nativeElement.value,
      text: this.text.nativeElement.value,
      tags: this.questionTags,
      creationDate: new Date()
    };
    
    this.questionsService.create(question).subscribe(result => {
      this.router.navigate(['questions']);
    }, error => console.log(error));
  }
}
