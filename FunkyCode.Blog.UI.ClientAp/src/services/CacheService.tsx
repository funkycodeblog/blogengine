export class CacheService<T> {

    private values: Map<string, T> = new Map<string, T>();
  
    public get(key: string): T | undefined {
      const hasKey = this.values.has(key);
      let entry: T | undefined;
      if (hasKey) {
        entry = this.values.get(key);
      }
  
      return entry;
    }
  
    public put(key: string, value: T) {
      this.values.set(key, value);
    }
  
  }