import { Injectable } from "@angular/core";
import { Tag } from "../models/tag.model";
import { BaseService } from "./base-service.service";

@Injectable()
export class TagsService extends BaseService<number, Tag> {
  route: string = "tags";
}
