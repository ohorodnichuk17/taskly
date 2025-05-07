export interface IAchievementsInitialState {
    achievements: IAchievement[] | null
}

export interface IAchievement {
    id: string,
    name: string,
    description: string,
    reward: number,
    percentageOfCompletion: number,
    icon: string,
    isCompleated: boolean
}