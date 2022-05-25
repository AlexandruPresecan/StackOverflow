import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Answer } from '../../../models/answer.model';
import { AnswersService } from '../../../services/answers.service';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css'],
  providers: [AnswersService]
})

export class AnswerComponent {

  @Input() answer!: Answer;
  @Input() allowVotes!: boolean;
  @Output() onVote = new EventEmitter();

  @ViewChild('text') text!: ElementRef;

  isEditing: boolean = false;

  constructor(private router: Router, private answersService: AnswersService) {

  }

  redirectToQuestion(): void {
    this.router.navigate(['questions', this.answer.questionId]);
  }

  redirectToAuthor(): void {
    this.router.navigate(['users', this.answer.author?.id]);
  }

  canEdit(): boolean {

    if (!localStorage["token"])
      return false;

    return localStorage["id"] == this.answer.author?.id || localStorage["admin"].toString() == "true";
  }

  editAnswer(): void {
    this.isEditing = !this.isEditing;
    this.text.nativeElement.value = this.answer.text;
  }

  updateAnswer(): void {

    this.isEditing = false;
    this.answer.text = this.text.nativeElement.value;

    this.answersService.update(this.answer.id, this.answer).subscribe(result => {
      this.onVote.emit();
    }, error => console.log(error));
  }

  deleteAnswer(): void {
    this.answersService.delete(this.answer.id).subscribe(result => {
      this.onVote.emit();
    }, error => console.log(error));
  }
}
