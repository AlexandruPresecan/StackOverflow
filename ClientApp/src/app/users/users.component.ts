import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent {

  public users: User[] = [];

  constructor(http: HttpClient, private router: Router, private route: ActivatedRoute) {
    http.get<User[]>('https://localhost:7001/api/applicationusers').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  public redirectToId(id: string): void {
    this.router.navigate([id], { relativeTo: this.route });
  }
}

interface User {
  id: string;
  userName: string;
  score: number;
}
