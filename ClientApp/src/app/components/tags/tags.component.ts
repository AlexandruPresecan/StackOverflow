import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})

export class TagsComponent {

  @Input() tags: string[] = [];

  constructor(private router: Router) {

  }

  redirectToTag(tag: string): void {
    this.router.navigate(['questions', 'tagged', tag]);
  }
}
