import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})

export class QuestionComponent {

  public question!: Question;

  constructor(http: HttpClient, route: ActivatedRoute) {
    http.get<Question>('https://localhost:7001/api/questions/' + route.snapshot.params.id).subscribe(result => {
      this.question = result;
    }, error => console.error(error));
  }
}

interface Question {
  title: string;
  text: string;
  author: User;
  tags: string[];
  voteCount: number;
  creationDate: Date;
  answers: Answer[];
}

interface Answer {
  text: string;
  creationDate: Date;
  author: User;
  voteCount: number;
}

interface User {
  id: string;
  userName: string;
  score: number;
}
