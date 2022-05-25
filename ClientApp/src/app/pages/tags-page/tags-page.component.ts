import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Tag } from '../../../models/tag.model';
import { TagsService } from '../../../services/tags.service';

@Component({
  selector: 'app-tags-page',
  templateUrl: './tags-page.component.html',
  styleUrls: ['./tags-page.component.css'],
  providers: [TagsService]
})

export class TagsPageComponent {

  tags!: Tag[];

  constructor(private tagsService: TagsService, private router: Router) {
    tagsService.getAll().subscribe(result => {
      this.tags = result;
    }, error => console.error(error));
  }

  redirectToTag(tag: string): void {
    this.router.navigate(['questions', 'tagged', tag]);
  }
}
