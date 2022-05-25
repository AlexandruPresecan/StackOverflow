import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
  providers: [UsersService]
})

export class RegisterPageComponent {

  @ViewChild('userName') userName!: ElementRef;
  @ViewChild('email') email!: ElementRef;
  @ViewChild('password') password!: ElementRef;
  @ViewChild('confirmPassword') confirmPassword!: ElementRef;

  error: string = "";

  constructor(private usersService: UsersService, private router: Router) {

  }

  registerUser(): void {

    this.error = "";

    const user: User = {
      id: "",
      score: 0,
      email: this.email.nativeElement.value,
      userName: this.userName.nativeElement.value,
      password: this.password.nativeElement.value,
      confirmPassword: this.confirmPassword.nativeElement.value,
      banned: false
    };

    this.usersService.register(user).subscribe(result => {
      this.router.navigate(['login'], { queryParams: { userName: user.userName } });
    }, error => this.error = error.error[0].description || error.error);
  }
}
