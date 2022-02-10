function solution(heights) {
    var answer = new Array(heights.length);
    answer[0] = 0
    for (let i = heights.length - 1; i > 0; i--) {
        if ( Math.max(...heights.slice(0, i)) < heights[i] ) {
            answer[i] = 0;
            continue;
        }
        for (let j = i - 1; j >= 0; j--) {
            if ( heights[j] > heights[i] ) {
                answer[i] = j + 1
                break;
            }
        }
        if (!answer[i]) {
            answer[i] = 0
        }
    }
    return answer;
}