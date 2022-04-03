import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})

export class CreateQuestionComponent {

  public question: Question = {
    title: "",
    text: "",
    authorId: 0,
    tags: [],
  };

  @ViewChild('tag') tag!: ElementRef;

  public addTag(): void {
    this.question.tags.push(this.tag.nativeElement.value);
    this.tag.nativeElement.value = "";
  }
}

interface Question {
  title: string;
  text: string;
  authorId: number;
  tags: string[];
}
