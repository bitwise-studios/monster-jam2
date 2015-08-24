using System;
public class SimpleGrid {
	private int columns, rows;
	private int[,] cells;
	public SimpleGrid(int w, int h, int[,] grid) {
		this.columns = w;
		this.rows = h;
		this.cells = grid;
	}
	public SimpleGrid Clone() {
		return new SimpleGrid(columns, rows, (int[,])cells.Clone());
	}

	// Computations
	public bool isLine(int row){
		for(var c = 0; c < this.columns; c++){
			if (this.cells[row, c] == 0){
				return false;
			}
		}
		return true;
	}

	public bool isEmptyRow(int row){
		for(var c = 0; c < this.columns; c++){
			if (this.cells[row, c] == 1){
				return false;
			}
		}
		return true;
	}

	public bool exceeded(){
		return !this.isEmptyRow(0) || !this.isEmptyRow(1);
	}

	public int height(){
		var r = 0;
		for(; r < this.rows && this.isEmptyRow(r); r++);
		return this.rows - r;
	}

	public int lines(){
		var count = 0;
		for(var r = 0; r < this.rows; r++){
			if (this.isLine(r)){
				count++;
			}
		}
		return count;
	}

	public int holes(){
		var count = 0;
		for(var c = 0; c < this.columns; c++){
			var block = false;
			for(var r = 0; r < this.rows; r++){
				if (this.cells[r, c] == 1) {
					block = true;
				}else if (this.cells[r, c] == 0 && block){
					count++;
				}
			}
		}
		return count;
	}

	public int blockades(){
		var count = 0;
		for(var c = 0; c < this.columns; c++){
			var hole = false;
			for(var r = this.rows - 1; r >= 0; r--){
				if (this.cells[r, c] == 0){
					hole = true;
				}else if (this.cells[r, c] == 1 && hole){
					count++;
				}
			}
		}
		return count;
	}

	public int aggregateHeight(){
		var total = 0;
		for(var c = 0; c < this.columns; c++){
			total += this.columnHeight(c);
		}
		return total;
	}

	public int bumpiness(){
		var total = 0;
		for(var c = 0; c < this.columns - 1; c++){
			total += Math.Abs(this.columnHeight(c) - this.columnHeight(c+ 1));
		}
		return total;
	}

	public int columnHeight(int column){
		var r = 0;
		for(; r < this.rows && this.cells[r, column] == 0; r++);
		return this.rows - r;
	}

	// Piece
	public void addPiece(AIPiece piece) {
		for(var r = 0; r < piece.cells.GetLength(0); r++) {
			for (var c = 0; c < piece.cells.GetLength(1); c++) {
				var _r = piece.row + r;
				var _c = piece.column + c;
				if (piece.cells[r, c] == 1 && _r >= 0){
					this.cells[_r, _c] = 1;
				}
			}
		}
	}

	public bool valid(AIPiece piece){
		for(var r = 0; r < piece.cells.GetLength(0); r++){
			for(var c = 0; c < piece.cells.GetLength(1); c++){
				var _r = piece.row + r;
				var _c = piece.column + c;
				if (piece.cells[r, c] == 1){
					if (!(_c < this.columns && _r < this.rows && this.cells[_r, _c] == 0)){
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool canMoveDown(AIPiece piece){
		for(var r = 0; r < piece.cells.GetLength(0); r++){
			for(var c = 0; c < piece.cells.GetLength(1); c++){
				var _r = piece.row + r + 1;
				var _c = piece.column + c;
				if (piece.cells[r, c] == 1 && _r >= 0){
					if (!(_r < this.rows && this.cells[_r, _c] == 0)){
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool canMoveLeft(AIPiece piece){
		for(var r = 0; r < piece.cells.GetLength(0); r++){
			for(var c = 0; c < piece.cells.GetLength(1); c++){
				var _r = piece.row + r;
				var _c = piece.column + c - 1;
				if (piece.cells[r, c] == 1){
					if (!(_c >= 0 && this.cells[_r, _c] == 0)){
						return false;
					}
				}
			}
		}
		return true;
	}

}