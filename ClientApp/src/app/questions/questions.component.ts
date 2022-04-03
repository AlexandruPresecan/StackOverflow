import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})

export class QuestionsComponent {

  @Input() questions: Question[] = [];

  constructor(http: HttpClient, private router: Router, private route: ActivatedRoute) {
    http.get<Question[]>('https://localhost:7001/api/questions').subscribe(result => {
      this.questions = result;
    }, error => console.error(error));
  }

  public redirectToId(id: number): void {
    this.router.navigate([id], { relativeTo: this.route });
  }

  public createQuestion(): void {
    this.router.navigate(['../', 'create-question'], { relativeTo: this.route });
  }
}

interface Question {
  id: number;
  title: string;
  text: string;
  author: User;
  tags: string[];
  voteCount: number;
  creationDate: Date;
}

interface User {
  id: string;
  userName: string;
  score: number;
}
