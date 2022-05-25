import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;
  @ViewChild('search') search!: ElementRef;

  constructor(private router: Router) {

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getLoginLogoutDisplay(): string {
    return localStorage["token"] ? "Logout" : "Login";
  }

  getRegisterAccountDisplay(): string {
    return localStorage["token"] ? localStorage["userName"] : "Register";
  }

  loginLogout(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    localStorage.clear();
  }

  registerAccount(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['register']);
      return;
    }

    this.router.navigate(['account']);
  }

  searchQuestion(): void {
    this.router.navigate(['questions'], { queryParams: { name: this.search.nativeElement.value } });
  }
}
