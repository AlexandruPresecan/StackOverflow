import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
  providers: [UsersService]
})

export class UserPageComponent {

  user!: User;

  constructor(private usersService: UsersService, private router: Router, private route: ActivatedRoute,) {

  }

  ngOnInit() {
    this.usersService.getById(this.route.snapshot.params.id).subscribe(result => {
      this.user = result;
    }, error => console.error(error));
  }

  isAdmin(): boolean {

    if (!localStorage["token"])
      return false;

    return localStorage["admin"].toString() == "true";
  }

  banUser(): void {

    if (this.user.banned)
      this.usersService.unban(this.user.id).subscribe(result => {
        this.ngOnInit();
      }, error => console.log(error));
    else
      this.usersService.ban(this.user.id).subscribe(result => {
        this.ngOnInit();
      }, error => console.log(error));
  }

  getBanValue(): string {
    return this.user.banned ? "Unban User" : "Ban User";
  }
}
