export interface Trait {
  name: string;
  description: string;
}

export interface Race {
  id: string;
  ownerId: string;
  name: string;
  traits: Trait[];
}
