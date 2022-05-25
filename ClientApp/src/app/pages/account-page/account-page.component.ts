import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.css'],
  providers: [UsersService]
})

export class AccountPageComponent {

  @ViewChild('userName') userName!: ElementRef;
  @ViewChild('email') email!: ElementRef;
  @ViewChild('newPassword') newPassword!: ElementRef;
  @ViewChild('confirmPassword') confirmPassword!: ElementRef;
  @ViewChild('password') password!: ElementRef;

  error: string = "";
  success: string = "";

  checks: Map<string, boolean> = new Map<string, boolean>();
  
  constructor(private usersService: UsersService, private router: Router) {
    this.checks.set("userName", false);
    this.checks.set("email", false);
    this.checks.set("password", false);
  }

  ngAfterViewInit() { 

    if (!localStorage["token"])
      return;

    this.email.nativeElement.value = localStorage["email"];
    this.userName.nativeElement.value = localStorage["userName"];
  }

  onCheck(field: string, event: Event): void {
    this.checks.set(field, (event.target as HTMLInputElement).checked);
  }

  updateUser(): void {

    if (!localStorage["token"])
      return;

    this.error = "";
    this.success = "";

    const user: User = {
      id: "",
      score: 0,
      email: this.checks.get("email") ? this.email.nativeElement.value : null,
      userName: this.checks.get("userName") ? this.userName.nativeElement.value : null,
      newPassword: this.checks.get("password") ? this.newPassword.nativeElement.value : null,
      confirmPassword: this.checks.get("password") ? this.confirmPassword.nativeElement.value : null,
      password: this.password.nativeElement.value,
      banned: false
    };

    this.usersService.update(localStorage["id"], user).subscribe(result => {
      this.success = result;
    }, error => { console.log(error); this.error = error.error[0].description || error.error });
  }
}
