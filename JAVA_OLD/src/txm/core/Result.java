/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package txm.core;

import txm.core.Player;
/**
 *
 * @author Sharpdeveloper
 */
public class Result {
    //enemy
    private Player enemy;
    public Player getEnemy(){
        return enemy;
    }
    //destroyedPoints
    private int destroyedPoints;
    public int getDestroyedPoints() {
        return destroyedPoints;
    }
    //lostPoints
    private int lostPoints;
    public int getLostPoints() {
        return lostPoints;
    }
    //maxPoints
    private int maxPoints;
    public int getMaxPoints() {
        return maxPoints;
    }
    
    //Constructor
    public Result(int _destroyedPoints, int _lostPoints, Player _enemy, int _maxPoints) {
        destroyedPoints = _destroyedPoints;
        lostPoints = _lostPoints;
        enemy = _enemy;
        maxPoints = _maxPoints;
    }
}
