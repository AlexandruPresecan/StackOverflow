import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [UsersService]
})

export class UsersComponent {

  public users: User[] = [];

  constructor(usersService: UsersService, private router: Router, private route: ActivatedRoute) {
    usersService.getAll().subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  public redirectToId(id: string): void {
    this.router.navigate([id], { relativeTo: this.route });
  }
}
