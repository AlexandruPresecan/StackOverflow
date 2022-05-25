import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  providers: [UsersService]
})

export class LoginPageComponent {

  @ViewChild('userName') userName!: ElementRef;
  @ViewChild('password') password!: ElementRef;

  error: string = "";
  userNameParam!: string;

  constructor(private usersService: UsersService, private router: Router, route: ActivatedRoute) {

    route.queryParams.subscribe(params => {

      if (params.userName)
        this.userNameParam = params.userName;
    });
  }

  ngAfterViewInit() {
    if (this.userNameParam)
      this.userName.nativeElement.value = this.userNameParam;
  }

  loginUser(): void {

    this.error = "";

    const user: User = {
      id: "",
      score: 0,
      userName: this.userName.nativeElement.value,
      password: this.password.nativeElement.value,
      banned: false
    };

    this.usersService.login(user).subscribe(result => {

      localStorage["token"] = result.token;
      localStorage["userName"] = result.userName;
      localStorage["email"] = result.email;
      localStorage["id"] = result.id;
      localStorage["admin"] = result.admin;

      this.router.navigate(['']);

    }, error => this.error = error.error);
  }
}
