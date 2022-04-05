import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [UsersService]
})

export class UserComponent {

  public user!: User;

  constructor(usersService: UsersService, private router: Router, private route: ActivatedRoute,) {
    usersService.getById(route.snapshot.params.id).subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }
}
