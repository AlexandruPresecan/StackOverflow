import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent {

  public user!: User;

  constructor(http: HttpClient, private router: Router, private route: ActivatedRoute, ) {
    http.get<User>('https://localhost:7001/api/applicationusers/' + route.snapshot.params.id).subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }

  public redirectToQuestion(id: number): void {
    this.router.navigate(['../../questions', id], { relativeTo: this.route });
  }
}

interface User {
  userName: string;
  score: number;
  questions: Question[];
  answers: Answer[];
}

interface Question {
  id: number;
  title: string;
  text: string;
  tags: string[];
  voteCount: number;
  creationDate: Date;
}

interface Answer {
  id: number;
  text: string;
  voteCount: number;
  creationDate: Date;
  questionId: number;
}
