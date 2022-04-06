import { Component, ElementRef, ViewChild } from '@angular/core';
import { Question } from '../../models/question.model';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})

export class CreateQuestionComponent {

  public question: Question = {
    id: 0,
    voteCount: 0,
    title: "",
    text: "",
    tags: [],
    creationDate: new Date()
  };

  @ViewChild('tag') tag!: ElementRef;

  public addTag(): void {
    this.question.tags.push(this.tag.nativeElement.value);
    this.tag.nativeElement.value = "";
  }
}
