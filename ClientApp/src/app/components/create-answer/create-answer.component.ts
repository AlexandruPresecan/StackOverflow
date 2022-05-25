import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Answer } from '../../../models/answer.model';
import { AnswersService } from '../../../services/answers.service';

@Component({
  selector: 'app-create-answer',
  templateUrl: './create-answer.component.html',
  styleUrls: ['./create-answer.component.css'],
  providers: [AnswersService]
})

export class CreateAnswerComponent {

  @Input() questionId!: number;
  @Output() onAnswerCreated  = new EventEmitter();

  @ViewChild('text') text!: ElementRef;

  constructor(private answersService: AnswersService, private router: Router) {

  }

  createAnswer(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    const answer: Answer = {
      id: 0,
      questionId: this.questionId,
      voteCount: 0,
      text: this.text.nativeElement.value,
      creationDate: new Date()
    };

    this.answersService.create(answer).subscribe(result => {
      this.onAnswerCreated.emit();
    }, error => console.log(error));
  }
}
